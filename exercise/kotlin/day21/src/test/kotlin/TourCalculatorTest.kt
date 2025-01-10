import arrow.core.Either
import arrow.core.left
import org.approvaltests.Approvals
import tour.NewTourCalculator
import tour.Step
import tour.TourCalculator
import java.time.Duration
import java.time.LocalTime
import java.time.temporal.ChronoUnit
import kotlin.test.Test
import kotlin.test.assertEquals

class TourCalculatorTest {

    @Test
    fun calculateGoldenMasterTest() {
        var refactored: Either<String, String> = "No calculations performed".left()

        val steps = mutableListOf<Step>()
        for (i in 0 until 30) {
            steps.add(Step(
                LocalTime.of(8, 30),
                "Location ${i}",
                DeliveryDuration(i * 26, ChronoUnit.MINUTES)))

            val origin = TourCalculator(steps).calculate();
            refactored = NewTourCalculator(steps).calculate();

            assertEquals(origin,refactored)
        }

        Approvals.verify(refactored)
    }

    private fun DeliveryDuration(duration: Int, unit: ChronoUnit)
        = Duration.of(duration.toLong(), unit).toSeconds().toInt()
}