package tour

import arrow.core.Either
import arrow.core.left
import arrow.core.right
import java.time.Duration
import java.time.LocalTime

data class Step(val time: LocalTime, val label: String, val deliveryTime: Int)

class NewTourCalculator(private var steps: List<Step>) {
    fun calculate(): Either<String, String> = if (steps.isNotEmpty()) {
        calculateSteps(steps.toSortedSteps()).right()
    } else "No locations !!!".left()

    private fun calculateSteps(steps: List<Step>): String = steps
        .logSteps()
        .appendDeliveryTime(steps)
        .toString()

    private fun fLine(step: Step): String = step.let { "${it.time} : ${it.label} | ${it.deliveryTime} sec" }
    private fun List<Step>.toSortedSteps(): List<Step> = this.filter { true }.sortedBy { it.time }
    private fun List<Step>.logSteps(): StringBuilder {
        val result = StringBuilder()
        this.forEach { step -> result.appendLine(fLine(step)) }
        return result
    }
    
    private fun StringBuilder.appendDeliveryTime(steps: List<Step>) =
        this.appendLine("Delivery time | ${formatDurationToHHMMSS(steps.sumOf { it.deliveryTime }.toLong())}")

    private fun formatDurationToHHMMSS(deliveryTime: Long): String {
        val duration = Duration.ofSeconds(deliveryTime)
        return "${duration.toHours()}%02d:${duration.toMinutesPart()}%02d:${duration.toSecondsPart()}%02d"
    }
}
